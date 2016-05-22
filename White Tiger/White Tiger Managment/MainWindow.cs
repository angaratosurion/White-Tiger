using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel.Discovery;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net;
using Hydrobase;
using HydrobaseSDK;
using HydroMultyUser;
namespace White_Tiger_Managment
{
    public partial class MainWindow : Form
    {
        public static string username, pass,passphrase;
        private int childFormNumber = 0;
        DiscoveryClient discoveryclient;
        HydroMultyUserCore multyuser = new HydroMultyUserCore();
        public MainWindow()
        {
            try
            {


                InitializeComponent();
                Program.CreateTempPath();
                multyuser.initialiseMultyUserInterface(false);

                MultyUser.CreateHydrobaseFileSystem(Environment.UserName);
                HydrobaseSDK.SDKBase.UserMainPath = MultyUser.GetUsersMainFolder(Environment.UserName);
                Maintenance_Backup mnt = new Maintenance_Backup();
                mnt.CreateFolder(Path.Combine(MultyUser.GetUsersMainFolder(Environment.UserName),BaseClass.tmpFolder));
                
                discoveryclient = new DiscoveryClient(new UdpDiscoveryEndpoint());
                FindResponse discoveryResponse = discoveryclient.Find(new FindCriteria(typeof(White_Tiger.IWhiteTigerService)));
                EndpointAddress address;
                if (discoveryResponse.Endpoints.Count > 0)
                {
                    address = discoveryResponse.Endpoints[0].Address;
                    Program.client = new White_TigerServiceReference.WhiteTigerServiceClient(new BasicHttpBinding(), address);

                }
                else
                {
                    Program.client = new White_TigerServiceReference.WhiteTigerServiceClient();
                }

                ;

                // MessageBox.Show(address.Uri.ToString() );


                BasicHttpBinding httpBinding = new BasicHttpBinding();
                httpBinding.MaxReceivedMessageSize = 2147483647;
                httpBinding.MaxBufferSize = 2147483647;
                BindingParameterCollection par = new BindingParameterCollection();
                par.Add(httpBinding);

                Program.client.Endpoint.Behaviors[0].AddBindingParameters(Program.client.Endpoint,par);
                

                

                Program.client.Open();
               // this.Text += "";

                //Program.client.Open();
                lblStatus.Text = Program.client.State.ToString();
                lblServerAddress.Text = Program.client.Endpoint.Address.ToString();
                lblServerVer.Text = Program.client.GetVersion();
                this.Text = Application.ProductName;
                aboutToolStripMenuItem.Text += "  " + Application.ProductName;
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

    

       

       

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void LoginMenu(object sender, EventArgs e)
        {
             try
            {
                Login login = new Login();
                login.Show();

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
        private void createDatabseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((username != null) &&(pass != null))
                {
                    string dbname;
                   dbname = Microsoft.VisualBasic.Interaction.InputBox("Type the Name of the Database", Application.ProductName, "",0,0);
                    if(dbname !=null)
                    {
                     Program.client.CreateDataBase(username,pass ,dbname );
                    }
                }
                else
                {

                    Login login = new Login();
                    login.Show();
                }

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

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((username != null) && (pass != null))
                {
                    TableCreation tablecreation = new TableCreation();
                    tablecreation.Show();
                }
                else
                {

                    Login login = new Login();
                    login.Show();
                }

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

        private void loadTableToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if ((MainWindow.username != null) && (MainWindow.pass != null))
                {
                    TableList dblis = new TableList();
                    dblis.Show();
                }
                else
                {
                    Login login = new Login();
                    login.Show();
                    TableList dblis = new TableList();
                    dblis.Show();
                  
                }

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

        private void decryptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if ((username != null) && (pass != null))
                {
                    /*string dbname;
                    dbname = Microsoft.VisualBasic.Interaction.InputBox("Type the Name of the Database", Application.ProductName, "", 0, 0);
                    if (dbname != null)
                    {
                        Program.client.CreateDataBase(username, pass, dbname);
                    }*/
                }
                else
                {

                    Login login = new Login();
                    login.Show();
                }

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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                AboutBox about = new AboutBox();
                about.Show();

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

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private void showHideToolStripMenuItem_Click(object sender, EventArgs ev)
        {
            try
            {
                int i;
                
                   
                        this.ShowInTaskbar = true;
                        //   tForm.MainMenuStrip.Items.Insert(tForm.FindToolStripMenuItem("βοήθειαToolStripMenuItem",tForm.MainMenuStrip), tForm.DefaultAdditionalCommandsMenu);
                       
                        this.WindowState = FormWindowState.Normal;




                   


                }


            
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }

        private void MainWindow_Resize(object sender, EventArgs ev)
        {
            try
            {
                
                    if ((this.WindowState == FormWindowState.Minimized))
                    {

                        this.ShowInTaskbar = false;
                        this.WindowState = FormWindowState.Minimized;
                        
                    }


               


            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }

        }

        private void serverUserManagmentToolStripMenuItem_Click(object sender, EventArgs ev)
        {
            try
            {

                if ((Program.client.State== CommunicationState.Opened) )
                {
                    if ((MainWindow.username != null) && (MainWindow.pass != null))
                    {
                        UserManagment usr = new UserManagment();
                        usr.Show();
                    }
                    else
                    {
                        Login login = new Login();
                        login.Show();
                        UserManagment usr = new UserManagment();
                        usr.Show();

                    }

                }





            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
