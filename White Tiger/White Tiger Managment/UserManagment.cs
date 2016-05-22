using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Hydrobase;
using White_Tiger.UserManagment;

namespace White_Tiger_Managment
{
    public partial class UserManagment : Form
    {
        hydrobaseADO ado = new hydrobaseADO();
        DataSet users = new DataSet();
        DataSet roles = new DataSet();
        Util util = new Util();
        string usersetdir = "";
        UserManager usermngr = new UserManager();
        const string userfile = "users.hbt";
        string userfilepath;

        public UserManagment()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserManagment_Load(object sender, EventArgs ev)
        {

            try
            {
                usersetdir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", ""),
        "WhiteTiger_Settings");
                userfilepath = Path.Combine(usersetdir, userfile);
               
                //  string cont=Program.client.GetUserList(MainWindow.username, MainWindow.pass);

                //if (cont !=null)
                {
                    // users.ReadXml(util.StringToStream(cont));
                    users= this.usermngr.LoadUsersToDataSet();
                    ado.SendtoDataGrid2(grdUsers, users, "users", 0);
                 
                    roles = this.usermngr.LoadRolesToDataSet();
                    ado.SendtoDataGrid2(grdRoles,  roles, "roles", 0);
                    //    roles.ReadXml(util.StringToStream())
                }


            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }

  
        private void btnSaveUsers_Click(object sender, EventArgs ev)
        {
            try
            {

                this.usermngr.UpdateUsers(this.users);

                //  string cont=Program.client.GetUserList(MainWindow.username, MainWindow.pass);

              
                    // users.ReadXml(util.StringToStream(cont));
                    //ado.SaveTable(users, userfilepath, 0, "---");
                 //   ado.SaveTimeDateinfo(userfilepath);
                   
               //  usermngr.S
                


            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }

        private void grdUsers_RowsAdded(object sender, DataGridViewRowsAddedEventArgs ev)
        {
            try
            {


                //  string cont=Program.client.GetUserList(MainWindow.username, MainWindow.pass);



            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }

        private void grdUsers_NewRowNeeded(object sender, DataGridViewRowEventArgs ev)
        {
            try
            {


                //  string cont=Program.client.GetUserList(MainWindow.username, MainWindow.pass);



            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }

        private void btnSaveRoles_Click(object sender, EventArgs ev)
        {
            try
            {

                this.usermngr.UpdateRoles(this.roles);
                
                //  string cont=Program.client.GetUserList(MainWindow.username, MainWindow.pass);


                // users.ReadXml(util.StringToStream(cont));
                //ado.SaveTable(users, userfilepath, 0, "---");
                //   ado.SaveTimeDateinfo(userfilepath);

                //  usermngr.S



            }
            catch (Exception e)
            {
                Program.errorreport(e);

            }
        }
    }
}
