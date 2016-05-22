using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Hydrobase;
using HydrobaseSDK;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using System.Reflection;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Microsoft.Win32;
using System.Windows.Forms;

namespace White_Tiger.UserManagment
{
    /// <summary>
    /// Class that manages the users of the pc
    /// </summary>
    public class UserManager
    {
        [System.Runtime.InteropServices.DllImport("advapi32.dll")]
        public static extern bool LogonUser(string userName, string domainName, string password, int LogonType, int LogonProvider, ref IntPtr phToken);


        static string[] usertable = { BaseClass.FileTagPrimKey + "User_Name", BaseClass.FileTagPrimKey + "Password",
                                        BaseClass.FileTagPrimKey + "Passphrase" };
        static string[] rolestable = { BaseClass.FileTagPrimKey + "User_Name", BaseClass.FileTagPrimKey + "Role",
                                        BaseClass.FileTagPrimKey + "Role_Type" };
        DataSet roles = new DataSet();
        Tools tools = new Tools();
        const string rolefile = "roles.hbt";

        static string rolefilepath;
        DataSet users = new DataSet();

        const string userfile = "users.hbt";
        hydrobaseADO ado = new hydrobaseADO();
        static string userfilepath, usersetdir;
        public UserManager()
        {
            MultyUser.ReadConfig();
          //  usersetdir = Path.Combine(MultyUser.GetUsersMainFolder("System"), "WhiteTiger", "Settings");
            usersetdir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase).Replace(@"file:\", "")
                , "WhiteTiger_Settings");
            userfilepath = Path.Combine(usersetdir, userfile);
          rolefilepath = Path.Combine(usersetdir, rolefile);
            if (!Directory.Exists(usersetdir))
            {
                Directory.CreateDirectory(usersetdir);
            }
            ado.TableCreation(userfilepath, usertable, "---");
            ado.TableCreation(rolefilepath, rolestable, "---");
            this.LoadRoles();
            this.LoadUsers();
            // ado.AttachDataBaseinDataSet(users,userfilepath);

        }
        #region users

        /// <summary>
        /// Loads the Users to the memory. It doesnt work with windows autherication
        /// </summary>
         void LoadUsers()
        {
            try
            {
              //  MessageBox.Show(userfilepath);
                //this.users = null;
                ado.AttachDataBaseinDataSet(users, userfilepath);
            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
         /// <summary>
         /// Loads the Users to the memory. It doesnt work with windows autherication
         /// 
         /// </summary>
         /// <returns> the users in a dataset</returns>
        public  DataSet LoadUsersToDataSet()
         {
             try
             {
                 //MessageBox.Show(userfilepath);
                 this.LoadUsers();
                 return users;
             }

             catch (Exception e)
             {

                 program.errorreport(e);
                 return null;

             }
         }
        /// <summary>
        /// Updates the Users Database
        /// </summary>
       public  void UpdateUsers()
        {
            try
            {

                ado.SaveTable(users, userfilepath, 0, "--");
                ado.SaveTimeDateinfo(userfilepath);
            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }

       /// <summary>
       /// Updates the Users Database uing the values from the given dataset
       /// </summary>
       /// <param name="set">Dataset containing the new values for the user's database</param>
        public void UpdateUsers(DataSet set)
       {
           try
           {
               if (set != null)
               {

                   ado.SaveTable(set, userfilepath, 0, "--");
                   ado.SaveTimeDateinfo(userfilepath);
                   foreach(DataRow row in set.Tables[0].Rows)
                   {
                       object[] vals = row.ItemArray;
                       string username = Convert.ToString(vals[0]);
                       string folder = MultyUser.GetUsersMainFolder(username);
                       if (!Directory.Exists(folder))
                       {
                           Directory.CreateDirectory(folder);
                       }
                       
                   }
               }
           }

           catch (Exception e)
           {

               program.errorreport(e);

           }
       }
       
        /// <summary>
        /// Closes the users database
        /// </summary>
     public  void CloseUsersTable()
        {
            try
            {

                ado.CloseDataBase(users);
            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="description">Description of user</param>
        /// <param name="hidden">true if you want to hide the user</param>
        public void CreateUser(string username, string password, string description, bool hidden)
        {
            try
            {

                this.CreateUser(username, password, description, hidden, password);
            }
            catch (PrincipalExistsException ex)
            {



            }
            catch (Exception e)
            {

                program.errorreport(e);

            }

        }
        // <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="description">Description of user</param>
        /// <param name="hidden">true if you want to hide the user</param>
        /// <param name="passphrase">The passphrase the user will have</param>
        public void CreateUser(string username, string password, string description, bool hidden, string passphrase)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {
                    if (((username != null) || (password != null)) && (WhiteTigerService.pref.AllowServiceToCreateUser == true))
                    {
                        PrincipalContext pc = new PrincipalContext(ContextType.Machine);

                        UserPrincipal u = new UserPrincipal(pc);

                        u.Name = username;
                        u.Description = description;
                        u.PasswordNeverExpires = true;
                        u.PasswordNotRequired = true;
                        u.SetPassword(password);

                        u.Save();



                        GroupPrincipal gPc = GroupPrincipal.FindByIdentity(pc, "Administrators");
                        gPc.Members.Add(u);

                        gPc.Save();

                        if (hidden == true)
                        {

                            RegistryKey regspecialacc = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\SpecialAccounts", true);
                            if (regspecialacc != null)
                            {
                                RegistryKey reguserlist = regspecialacc.OpenSubKey("UserList", true);
                                if (reguserlist != null)
                                {

                                    reguserlist.SetValue(username, 0, RegistryValueKind.DWord);
                                }
                                else
                                {

                                    reguserlist = regspecialacc.CreateSubKey("UserList");
                                    reguserlist.SetValue(username, 0, RegistryValueKind.DWord);


                                }

                            }
                            else
                            {
                                RegistryKey regwinloagon = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);
                                if (regwinloagon != null)
                                {
                                    regwinloagon.CreateSubKey("SpecialAccounts");


                                    RegistryKey reguserlist = regspecialacc.CreateSubKey("UserList");
                                    reguserlist.SetValue(username, 0, RegistryValueKind.DWord);





                                }



                            }
                        }

                    }


                }

                else
                {
                    if ((username != null) && ((password != null)) || (passphrase != null))
                    {
                       if (this.FindUser(username) == null)
                        {
                            this.LoadUsers();
                            if ((users != null) && (users.Tables != null))
                            {
                                object[] vals = new object[usertable.Length];
                                vals[0] = username;
                                vals[1] = password;
                                vals[2] = passphrase;
                                users.Tables[0].Rows.Add(vals);
                                ado.SaveTable(users, userfilepath, 0, "---");

                            }
                        }
                    }
                }
                string folder = MultyUser.GetUsersMainFolder(username);
                if (!Directory.Exists(folder))
                {
                    MultyUser.AddNewUser(username);
                }
            }
            catch (PrincipalExistsException ex)
            {



            }
            catch (Exception e)
            {

                program.errorreport(e);

            }

        }
        /// <summary>
        /// Authericates the user
        /// </summary>
        /// <param name="username">user's name</param>
        /// <param name="password">user's password</param>
        /// <returns>true if is successfull false otherwise</returns>
        public bool Login(string username, string password)
        {
            try
            {
                bool ap = false;

                if (username != null && password != null)
                {
                    if (WhiteTigerService.pref.WindowsAutherication)
                    {
                        string domain = Environment.UserDomainName;

                        IntPtr tokenHandler = IntPtr.Zero;

                        ap = LogonUser(username, domain, password, 2, 0, ref tokenHandler);
                    }
                    else
                    {
                        DataRow row = this.FindUser(username);
                        if (row != null)
                        {
                            object[] vals = row.ItemArray;
                            if (Convert.ToString(vals[0]) == username && Convert.ToString(vals[1]) == password)
                            {
                                ap = true;
                            }

                        }

                    }
                }

                return ap;

            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                return false;
            }

        }

        /// <summary>
        /// Search fo a user with the given username
        /// </summary>
        /// <param name="username">the name of the user</param>
        /// <returns>the row containing information about the user</returns>
        public DataRow FindUser(String username)
        {
            try
            {
                DataRow row = null;
                if (username != null)
                {
                    if (users != null && users.Tables.Count > 0)
                    {
                        row = ado.Search2(users, usertable[0], username, "users", 0)[0];
                    }
                }

                return row;

            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                return null;
            }
        }
        // <summary>
        /// edits the user user
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>       
        /// <param name="newpass">New password </param>
        /// <param name="passphrase">The passphrase the user will have</param>
        /// <param name="hidden">true if you want to hide the user</param>
        public void EditUser(string username, string password, string newpass, string passphrase, bool hidden)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {
                    if (((username != null) || (password != null)) && (WhiteTigerService.pref.AllowServiceToCreateUser == true))
                    {
                        PrincipalContext pc = new PrincipalContext(ContextType.Machine);

                        UserPrincipal u = new UserPrincipal(pc);

                        u.Name = username;
                        // u.Description = description;
                        u.PasswordNeverExpires = true;
                        u.PasswordNotRequired = true;
                        u.SetPassword(password);

                        u.Save();



                        GroupPrincipal gPc = GroupPrincipal.FindByIdentity(pc, "Administrators");
                        gPc.Members.Add(u);

                        gPc.Save();

                        if (hidden == true)
                        {

                            RegistryKey regspecialacc = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\SpecialAccounts", true);
                            if (regspecialacc != null)
                            {
                                RegistryKey reguserlist = regspecialacc.OpenSubKey("UserList", true);
                                if (reguserlist != null)
                                {

                                    reguserlist.SetValue(username, 0, RegistryValueKind.DWord);
                                }
                                else
                                {

                                    reguserlist = regspecialacc.CreateSubKey("UserList");
                                    reguserlist.SetValue(username, 0, RegistryValueKind.DWord);


                                }

                            }
                            else
                            {
                                RegistryKey regwinloagon = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);
                                if (regwinloagon != null)
                                {
                                    regwinloagon.CreateSubKey("SpecialAccounts");


                                    RegistryKey reguserlist = regspecialacc.CreateSubKey("UserList");
                                    reguserlist.SetValue(username, 0, RegistryValueKind.DWord);





                                }



                            }
                        }

                    }


                }

                else
                {
                    if ((username != null) && ((password != null)) || (passphrase != null))
                    {
                        DataRow row = this.FindUser(username);
                        if (row != null)
                        {
                            if ((users != null) && (users.Tables != null))
                            {
                                object[] vals = new object[usertable.Length];
                                vals[0] = username;
                                vals[1] = newpass;
                                vals[2] = passphrase;
                                users.Tables[0].Rows.Remove(row);
                                users.Tables[0].Rows.Add(vals);
                                //ado.SaveTable(users, userfilepath, 0, "---");
                                this.UpdateUsers();
                                this.CloseUsersTable();
                                this.LoadUsers();

                            }
                        }
                    }
                }
            }
            catch (PrincipalExistsException ex)
            {



            }
            catch (Exception e)
            {

                program.errorreport(e);

            }

        }

        // <summary>
        /// deletes the user user
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>       
        /// <param name="usertodelete">the user which is to be deleted </param>
        public void DeleteUser(string username, string password, string usertodelete)
        {
            try
            {
                if ((username != null) && ((password != null)) || (usertodelete != null))
                {
                    DataRow row = this.FindUser(usertodelete);
                    if (this.Login(username, password) && row != null)
                    {
                        if ((users != null) && (users.Tables != null))
                        {

                            users.Tables[0].Rows.Remove(row);
                            this.UpdateUsers();
                            this.CloseUsersTable();
                            this.LoadUsers();
                            string folder = MultyUser.GetUsersMainFolder(usertodelete);
                            if (Directory.Exists(folder))
                            {
                                Directory.Delete(folder, true);
                            }

                        }
                    }
                }

            }
            catch (Exception e)
            {
                program.errorreport(e);
            }
        }

        /// <summary>
        /// Gets the list of users
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="pass">password</param>    
        /// <returns>the list of users</returns>
        public DataRowCollection GetUserList(string username, string pass)
        {
            try
            {
                DataRowCollection row = null;
                if (username != null && this.Login(username, pass))
                {
                    this.CloseUsersTable();
                    this.LoadUsers();
                    row = this.users.Tables[0].Rows;
                }

                return row;

            }
            catch (Exception ex)
            {
                program.errorreport(ex);
                return null;
            }
        }
        #endregion

        #region Roles

        /// <summary>
        /// Loads the Roles to the memory. It doesnt work with windows autherication
        /// 
        /// </summary>
        /// <returns> the roles in a dataset</returns>
        public DataSet LoadRolesToDataSet()
        {
            try
            {
                this.LoadRoles();
                return roles;
            }

            catch (Exception e)
            {

                program.errorreport(e);
                return null;

            }
        }
        
        void LoadRoles()
        {
            try
            {

                ado.AttachDataBaseinDataSet(roles, rolefilepath);
            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
        /// <summary>
        /// Updates the Roles Database
        /// </summary>
       public void UpdateRoles()
        {
            try
            {

                ado.SaveTable(roles, rolefilepath, 0, "--");
                ado.SaveTimeDateinfo(rolefilepath);
            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
       /// <summary>
       /// Updates the Roles Database using values from the given dataset
       /// </summary>
       /// <param name="set"> Dataset containing the new values</param>
       public void UpdateRoles(DataSet set )
       {
           try
           {
               if (set != null)
               {
                   ado.SaveTable(set, rolefilepath, 0, "--");
                   ado.SaveTimeDateinfo(rolefilepath);
               }
           }

           catch (Exception e)
           {

               program.errorreport(e);

           }
       }
        /// <summary>
        /// Closes the users database
        /// </summary>
      public  void CloseRolesTable()
        {
            try
            {

                ado.CloseDataBase(roles);
            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
        /// <summary>
        /// Creates a new Role
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the new role</param>
        /// <param name="type">type of the new role</param>
        public void CreateRole(string username, string password, string role, RoleType type)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null) ))
                    {
                        this.LoadRoles();

                        if ((roles != null) && (roles.Tables != null) &&this.Login(username,password) && 
                            this.IsUserOnARole(username,password,"Administrator"))
                        {
                            object[] vals = new object[rolestable.Length];
                            vals[0] = username;
                            vals[1] = role;
                            vals[2] = type.ToString();
                            roles.Tables[0].Rows.Add(vals);
                            this.UpdateRoles();
                            this.CloseRolesTable();
                            this.LoadRoles();

                        }
                    }
                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
        /// <summary>
        /// Creates a new Role for 1st time install only
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the new role</param>
        /// <param name="type">type of the new role</param>
        public void CreateRole1stTime(string username, string password, string role, RoleType type)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null)))
                    {
                        this.LoadRoles();

                        if ((roles != null) && (roles.Tables != null) && this.Login(username, password) )
                        {
                            object[] vals = new object[rolestable.Length];
                            vals[0] = username;
                            vals[1] = role;
                            vals[2] = type.ToString();
                            roles.Tables[0].Rows.Add(vals);
                            this.UpdateRoles();
                            this.CloseRolesTable();
                            this.LoadRoles();

                        }
                    }
                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
     
        /// <summary>
        /// Finds a Role with the given name
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="Role">name of the role</param>
        /// <returns> Role with the given name</returns>
        public DataRow FindRole(string username, string password, string Role)
        {
            try
            {
                DataRow ap = null;
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null)) &&this.Login(username,password)
                         && this.IsUserOnARole(username,password,"Administrator"))
                    {

                        if ((roles != null) && (roles.Tables != null))
                        {
                            ap = ado.Search2(roles, rolestable[1], Role, "roles", 0)[0];
                        }
                    }
                }
                return ap;


            }

            catch (Exception e)
            {

                program.errorreport(e);
                return null;

            }

        }
        /// <summary>
        /// Edit a new Role
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the new role</param>
        /// <param name="type">type of the new role</param>
        public void EditRole(string username, string password, string role, RoleType type)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null) &&this.Login(username,password) 
                         && this.IsUserOnARole(username,password,"Administrator")))
                    {

                        if ((roles != null) && (roles.Tables != null))
                        {
                            DataRow row = this.FindRole(username, password, role);
                            if (role != null)
                            {

                                this.DeleteRole(username, password, role);
                                this.CreateRole(username, password, role, type);

                                this.UpdateRoles();
                                this.CloseRolesTable();
                                this.LoadRoles();
                            }

                        }
                    }
                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }

        /// <summary>
        /// deletes a  Role
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the new role</param>
       
        public void DeleteRole(string username, string password, string role)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null) && this.Login(username, password)
                        && this.IsUserOnARole(username,password,"Administrator")))
                    {

                        if ((roles != null) && (roles.Tables != null))
                        {
                            DataRow row = this.FindRole(username, password, role);
                            if (role != null)
                            {
                                this.roles.Tables[0].Rows.Remove(row);


                                this.UpdateRoles();
                                this.CloseRolesTable();
                                this.LoadRoles();
                            }

                        }
                    }
                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
        /// <summary>
        /// Attaches a user to a Role
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the new role</param>
        public void AttachaUserToRole(string username, string password, string role)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null)))
                    {

                        if ((roles != null) && (roles.Tables != null) && this.Login(username, password))
                        {
                            DataRow row = this.FindRole(username, password, role);
                            if (row != null)
                            {

                                object[] vals = new object[rolestable.Length];
                                vals[0] = username;
                                vals[1] = role;
                                vals[2] = tools.GetUserRoleEnum(Convert.ToString(row.ItemArray[2])).ToString();
                                roles.Tables[0].Rows.Add(vals);
                            }
                            else
                            {
                                object[] vals = new object[rolestable.Length];
                                vals[0] = username;
                                vals[1] = role;
                                vals[2] = RoleType.Administrator.ToString();

                                roles.Tables[0].Rows.Add(vals);
                            }
                    
                            this.UpdateRoles();
                            this.CloseRolesTable();
                            this.LoadRoles();

                        }
                    }
                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }

        /// <summary>
        /// Chescks to see if  a userbelongs to the given a Role
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the  role</param>
        /// <returns>True if it belongs</returns>
        public bool IsUserOnARole(string username, string password, string role)
        {
           try
           {
               bool ap = false;

                 if ((username != null) && ((password != null)))
                    {

                        if ((roles != null) && (roles.Tables != null) && this.Login(username, password))
                        {
                            DataRow row = this.FindRole(username, password, role);
                            if (row != null)
                            {

                                object[] vals = new object[rolestable.Length];
                                DataRowCollection r= this.ado.Search2(roles,rolestable[0],username,"roles",0);
                                if( r!=null)
                                {
                                    foreach( DataRow it in r)
                                    {
                                        object[] v = it.ItemArray;
                                        if( Convert.ToString(v[1])==role)
                                        {
                                            ap = true;
                                            break;
                                        }
                                    }

                                }



                            }
                            
                    
                            

                        }
                    }

                 return ap;

            }

            catch (Exception e)
            {

                program.errorreport(e);
                return false;
            }

        }

        /// <summary>
        /// detaches a user from a Role
        /// </summary>
        /// <param name="username">name of user</param>
        /// <param name="password">password</param>
        /// <param name="role">name of the new role</param>

        public void DetachaUserFromRole(string username, string password, string role)
        {
            try
            {
                if (White_Tiger.WhiteTigerService.pref.WindowsAutherication)
                {


                }
                else
                {
                    if ((username != null) && ((password != null) && this.Login(username, password)
                        && this.IsUserOnARole(username, password, "Administrator")))
                    {

                        if ((roles != null) && (roles.Tables != null))
                        {
                            DataRow row = this.FindRole(username, password, role);
                            if (role != null)
                            {
                                this.roles.Tables[0].Rows.Remove(row);


                                this.UpdateRoles();
                                this.CloseRolesTable();
                                this.LoadRoles();
                            }

                        }
                    }
                }


            }

            catch (Exception e)
            {

                program.errorreport(e);

            }
        }
        




        #endregion
    }
}